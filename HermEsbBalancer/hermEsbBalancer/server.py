import signal
import sys
from time import sleep
from hermEsbBalancer.handlers import HandlerRepository
from hermEsbBalancer.balancer import Balancer

__author__ = 'Sergio'
import logging
import logging.config
from optparse import OptionParser

import yaml

from config import Config
bal = None

def signal_handler(self, signal, frame):
    if bal:
        bal.stop()
    sys.exit(0)

if __name__ == "__main__":
    signal.signal(signal.SIGTERM, signal_handler)
    parser = OptionParser()
    parser.add_option("-c", "--config", dest="configfile",default="balancer.cfg",
                      help="Configuration File", metavar="FILE")
    parser.add_option("-l", "--log", dest="logfile",default="logging.yml",
                      help="Configuration File")

    (options, args) = parser.parse_args()

    cfg = Config(options.configfile)

    logcfg = yaml.load(open(options.logfile, 'r'))
    logging.config.dictConfig(logcfg)
    Balancer.Create(cfg, HandlerRepository.Instance())
    Balancer.Instance().start()
    if sys.platform != "win32":
        signal.pause()
    else:
        try:
            sleep(-1)
        except KeyboardInterrupt:
            Balancer.Instance().stop()
    sys.exit(0)

