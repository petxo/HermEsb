import logging

__author__ = 'sergio'


def getlogger():
    return logging.getLogger("hermEsbBalancer")


def get_core_logger():
    return logging.getLogger("hermEsbBalancer.core")


def get_endPoints_logger():
    return logging.getLogger("hermEsbBalancer.endpoints")
