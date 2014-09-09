from datetime import datetime, timedelta

__author__ = 'Sergio'
import re
from bson import ObjectId
import pymongo


class ServiceRepository:
    def __init__(self, server):
        self.__conn = pymongo.MongoClient(host=server, max_pool_size="10")
        self.__db = self.__conn["BusStatistics"]["ServiceInfo"]
        self.__db.ensure_index([("UtcTimeTakenSample", pymongo.ASCENDING)], background=True)
        self.__db.ensure_index([("MemoryWorkingSet", pymongo.DESCENDING)], background=True)
        self.__db.ensure_index([("Latency", pymongo.DESCENDING)], background=True)
        self.__db.ensure_index([("PeakMaxLatency", pymongo.DESCENDING)], background=True)

    def GetServicesNotResponding(self, seconds, rows):
        time = datetime.utcnow() - timedelta(0, seconds)
        return self.__db.find({"UtcTimeTakenSample": {"$lt": time.strftime("%Y/%m/%d %H:%M:%S")}})\
            .sort("UtcTimeTakenSample", pymongo.ASCENDING) \
            .limit(rows)

    def GetCountServicesNotResponding(self, seconds):
        time = datetime.utcnow() - timedelta(0, seconds)
        return self.__db.find({"UtcTimeTakenSample": {"$lt": time.strftime("%Y/%m/%d %H:%M:%S")}}).count()

    def GetMemoryTop(self, rows):
        return self.__db.find()\
            .sort("MemoryWorkingSet", pymongo.DESCENDING) \
            .limit(rows)

    def GetLatencyTop(self, rows):
        return self.__db.find()\
            .sort("Latency", pymongo.DESCENDING) \
            .limit(rows)

    def GetLatencyPeakTop(self, rows):
        return self.__db.find()\
            .sort("PeakMaxLatency", pymongo.DESCENDING) \
            .limit(rows)

    def GetLoadQueueTop(self, rows):
        return self.__db.find({"TotalMessages": {"$gte": 0}})\
            .sort("TotalMessages", pymongo.DESCENDING) \
            .limit(rows)


class ServiceInfoView:
    def __init__(self, server):
        self.__repo = ServiceRepository(server)
        self.__map = {"Service": "ServiceId._id", "HandlerType": "HandlerType", "Error": "Exception.Message"}

    def GetServicesNotResponding(self, seconds, rows):
        rowlist = list()

        try:
            total = self.__repo.GetCountServicesNotResponding(seconds)
            mongoCursor = self.__repo.GetServicesNotResponding(seconds, rows)

            for p in mongoCursor:
                pmin = list()
                pmin.append(p["Identification"]["_id"])
                pmin.append(p["UtcTimeTakenSample"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return {"total": total, "rows": rowlist}

    def MemoryTop(self, rows):
        rowlist = list()
        try:
            rowlist.append(['Service', 'Memory'])
            mongoCursor = self.__repo.GetMemoryTop(rows)
            for p in mongoCursor:
                pmin = list()
                pmin.append(p["Identification"]["_id"])
                pmin.append(p["MemoryWorkingSet"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist

    def LantecyTop(self, rows):
        rowlist = list()
        try:
            rowlist.append(['Service', 'Latency', 'Velocity'])
            mongoCursor = self.__repo.GetLatencyTop(rows)
            for p in mongoCursor:
                pmin = list()
                pmin.append(p["Identification"]["_id"])
                pmin.append(p["Latency"] / 1000)
                pmin.append(p["Velocity"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist

    def LantecyPeakTop(self, rows):
        rowlist = list()
        try:
            rowlist.append(['Service', 'PeakMaxLatency', 'PeakMinLatency'])
            mongoCursor = self.__repo.GetLatencyPeakTop(rows)
            for p in mongoCursor:
                pmin = list()
                pmin.append(p["Identification"]["_id"])
                pmin.append(p["PeakMaxLatency"] / 1000)
                pmin.append(p["PeakMinLatency"] / 1000)

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist

    def LoadQueueTop(self, rows):
        rowlist = list()
        try:
            rowlist.append(['Service', 'Messages', 'Velocity'])
            mongoCursor = self.__repo.GetLoadQueueTop(rows)
            for p in mongoCursor:
                pmin = list()
                pmin.append(p["Identification"]["_id"])
                pmin.append(p["TotalMessages"])
                pmin.append(p["Velocity"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist