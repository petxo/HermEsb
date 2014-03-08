from datetime import datetime, timedelta

__author__ = 'Sergio'
import re
import pymongo


class StatisticsRepository:
    def __init__(self, server):
        self.__conn = pymongo.MongoClient(host=server, max_pool_size="10")
        self.__dbHealth = self.__conn["BusStatistics"]["HealthEntity"]
        self.__dbSpeed = self.__conn["BusStatistics"]["ProcessingVelocityEntity"]
        self.__dbQueue = self.__conn["BusStatistics"]["QueueLoadEntity"]
        self.__dbBand = self.__conn["BusStatistics"]["TransferVelocityEntity"]

        self.__dbHealth.ensure_index([("Identification._id", pymongo.ASCENDING),
                                      ("UtcTimeTakenSample", pymongo.ASCENDING)], background=True)
        self.__dbSpeed.ensure_index([("Identification._id", pymongo.ASCENDING),
                                      ("UtcTimeTakenSample", pymongo.ASCENDING)], background=True)
        self.__dbBand.ensure_index([("Identification._id", pymongo.ASCENDING),
                                      ("UtcTimeTakenSample", pymongo.ASCENDING)], background=True)
    def GetMemory(self, id, seconds):
        start = datetime.utcnow() - timedelta(0, seconds)
        return self.__dbHealth.find({"Identification._id": id,
                                    "UtcTimeTakenSample": {"$gt": start}})\
            .sort("UtcTimeTakenSample", pymongo.ASCENDING)

    def GetSpeed(self, id, seconds):
        start = datetime.utcnow() - timedelta(0, seconds)
        return self.__dbSpeed.find({"Identification._id": id,
                                    "UtcTimeTakenSample": {"$gt": start}})\
            .sort("UtcTimeTakenSample", pymongo.ASCENDING)

    def GetLatency(self, id, seconds):
        start = datetime.utcnow() - timedelta(0, seconds)
        return self.__dbSpeed.find({"Identification._id": id,
                                    "UtcTimeTakenSample": {"$gt": start}})\
            .sort("UtcTimeTakenSample", pymongo.ASCENDING)

    def GetLoadQueue(self, id, seconds):
        start = datetime.utcnow() - timedelta(0, seconds)
        return self.__dbQueue.find({"Identification._id": id,
                                    "UtcTimeTakenSample": {"$gt": start}})\
            .sort("UtcTimeTakenSample", pymongo.ASCENDING)

    def GetBandWidth(self, id, seconds):
        start = datetime.utcnow() - timedelta(0, seconds)
        return self.__dbBand.find({"Identification._id": id,
                                    "UtcTimeTakenSample": {"$gt": start}})\
            .sort("UtcTimeTakenSample", pymongo.ASCENDING)

    def GetTotalBandWidth(self, id, seconds):
        start = datetime.utcnow() - timedelta(0, seconds)
        return self.__dbBand.find({"Identification._id": id,
                                    "UtcTimeTakenSample": {"$gt": start}})\
            .sort("UtcTimeTakenSample", pymongo.ASCENDING)


class StatisticsView:
    def __init__(self, server):
        self.__repo = StatisticsRepository(server)
        self.__hora = re.compile(r'(\d\d:\d\d:\d\d)')

    def GetMemory(self, id, seconds):
        rowlist = list()
        rowlist.append(['Hora', 'Memoria'])
        try:
            mongoCursor = self.__repo.GetMemory(id, seconds)

            for p in mongoCursor:
                pmin = list()
                pmin.append(p["UtcTimeTakenSample"].strftime("%H:%M"))
                pmin.append(p["MemoryWorkingSet"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist

    def GetSpeed(self, id, seconds):
        rowlist = list()
        rowlist.append(['Hora', 'Mensajes'])
        try:
            mongoCursor = self.__repo.GetSpeed(id, seconds)

            for p in mongoCursor:
                pmin = list()
                pmin.append(p["UtcTimeTakenSample"].strftime("%H:%M"))
                pmin.append(p["Velocity"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist

    def GetLatency(self, id, seconds):
        rowlist = list()
        rowlist.append(['Hora', 'Latencia'])
        try:
            mongoCursor = self.__repo.GetLatency(id, seconds)

            for p in mongoCursor:
                pmin = list()
                pmin.append(p["UtcTimeTakenSample"].strftime("%H:%M"))
                pmin.append(p["Latency"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist

    def GetBandWidth(self, id, seconds):
        rowlist = list()
        rowlist.append(['Hora', 'Input', 'Output'])
        try:
            mongoCursor = self.__repo.GetBandWidth(id, seconds)

            for p in mongoCursor:
                pmin = list()
                pmin.append(p["UtcTimeTakenSample"].strftime("%H:%M"))
                pmin.append(p["Input"]["Speed"])
                pmin.append(p["Output"]["Speed"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist

    def GetTotalBandWidth(self, id, seconds):
        rowlist = list()
        rowlist.append(['Hora', 'Input', 'Output'])
        try:
            mongoCursor = self.__repo.GetTotalBandWidth(id, seconds)

            for p in mongoCursor:
                pmin = list()
                pmin.append(p["UtcTimeTakenSample"].strftime("%H:%M"))
                pmin.append(p["Input"]["Total"])
                pmin.append(p["Output"]["Total"])

                rowlist.append(pmin)

        except Exception as ex:
            pass

        return rowlist