# author = sbermudel
# package description
import re
from bson import ObjectId
import pymongo



class ErroresRepository:
    def __init__(self, server):
        self.__conn = pymongo.MongoClient(host=server, max_pool_size="10")
        self.__db = self.__conn["BusStatistics"]["ErrorHandlerEntity"]

    def GetErrors(self, page, rows, flt):
        skip = (page - 1) * rows
        search = dict()
        for f in flt:
            date_re = re.compile(r'.*' + flt[f] + ".*")
            search[f] = {'$regex': date_re}

        return self.__db.find(search)\
                    .sort("UtcSuccessAt", pymongo.DESCENDING)\
                    .limit(rows)\
                    .skip(skip)

    def GetCountErrors(self, flt):
        search = dict()
        for f in flt:
            date_re = re.compile(r'.*' + flt[f] + ".*")
            search[f] = {'$regex': date_re}

        return self.__db.find(search).count()

    def GetError(self, id):
        return self.__db.find_one(ObjectId(id))


class ErroresView:
    def __init__(self, server):
        self.__repo = ErroresRepository(server)
        self.__map = {"Service" : "ServiceId._id", "HandlerType" : "HandlerType", "Error" : "Exception.Message"}


    def GetErrors(self, page, rows, flt):
        rowlist = list()

        search = dict()
        for f in flt:
            search[self.__map[f]] = flt[f]

        try:
            total = self.__repo.GetCountErrors(search)
            mongoCursor = self.__repo.GetErrors(page, rows, search)

            for p in mongoCursor:
                pmin = dict()
                pmin["Id"] = str(p["_id"])
                pmin["Actions"] = str(p["_id"])
                pmin["Service"] = p["ServiceId"]["_id"]
                pmin["Fecha"] = p["UtcSuccessAt"]
                pmin["HandlerType"] = p["HandlerType"]
                pmin["Error"] = self.GetException(p["Exception"])

                rowlist.append(pmin)
        except Exception as ex:
            pass

        ret = dict()
        ret["page"] = page
        ret["records"] = total
        ret["total"] = int(total / rows)
        ret["rows"] = rowlist

        return ret

    def GetError(self, id):
        error = self.__repo.GetError(id)
        errorView = dict()
        errorView["Id"] = str(error["_id"])
        errorView["Service"] = error["ServiceId"]["_id"]
        errorView["Fecha"] = error["UtcSuccessAt"]
        errorView["HandlerType"] = error["HandlerType"]
        errorView["Error"] = self.GetException(error["Exception"])
        errorView["StackTrace"] = self.GetStack(error["Exception"])
        errorView["Message"] = error["Message"]
        errorView["Header"] = error["Header"]

        return errorView

    def GetException(self, exception):
        ex = exception["Message"]
        if not exception["InnerException"] is None:
            ex = ex + "\r\n" + self.GetException(exception["InnerException"])
        return ex

    def GetStack(self, exception):
        ex = exception["StackTrace"]
        if not exception["InnerException"] is None:
            ex = self.GetStack(exception["InnerException"]) + ex + "\r\n"
        return ex

    def GetMessage(self, id):
        error = self.__repo.GetError(id)
        return error["Message"]