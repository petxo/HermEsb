import re
from bson import ObjectId

__author__ = 'sbermudel'

import json
import datetime

DEFAULT_DATE_FORMAT = '%a, %d %b %Y %H:%M:%S UTC'
DEFAULT_ARGUMENT = "datetime_format"


class JsonNetEncoder(json.JSONEncoder):
    def default(self, obj):
        if isinstance(obj, datetime.datetime):
            delta = obj - datetime.datetime(1970, 1, 1)
            return "/Date(%d)/" % (delta.total_seconds() * 1000)
        elif isinstance(obj, ObjectId):
            return {"_id": str(obj)}
        else:
            return super(JsonNetEncoder, self).default(obj)


def loads(s, **kwargs):
    format = kwargs.pop(DEFAULT_ARGUMENT, None) or DEFAULT_DATE_FORMAT
    source = json.loads(s, **kwargs)

    return iteritems(source, format)


def iteritems(source, format):
    for k, v in source.items():
        if isinstance(v, dict):
            iteritems(v, format)
        elif isinstance(v, basestring):
            match = re.search('^/Date\((\d+)\+?\d+?\)/', v)
            if not match is None:
                source[k] = datetime.datetime(1970, 1, 1) + datetime.timedelta(milliseconds=long(match.group(1)))
                pass
            else:
                try:
                    source[k] = datetime.datetime.strptime(v, format)
                except:
                    pass

    return source


def dumps(obj, skipkeys=False, ensure_ascii=True, check_circular=True,
          allow_nan=True, cls=JsonNetEncoder, indent=None, separators=None,
          encoding='utf-8', default=None, **kw):
    return json.dumps(obj, skipkeys, ensure_ascii, check_circular,
                      allow_nan, cls, indent, separators,
                      encoding, default, **kw)



