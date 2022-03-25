using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap
{
    class CharsOfObjects
    {
        private FixedObjectFactory _ObjFactory;
        private Dictionary<char, Factory> _availableCharsOfObjs;

        public CharsOfObjects()
        {
            _ObjFactory = new();
            _availableCharsOfObjs = new()
            {
                ['.'] = _ObjFactory,
                ['|'] = _ObjFactory,
                ['_'] = _ObjFactory,
                ['I'] = _ObjFactory,
                ['/'] = _ObjFactory,
                ['o'] = _ObjFactory,

                ['h'] = _ObjFactory,
                ['X'] = _ObjFactory,
                ['D'] = _ObjFactory,
                ['Y'] = _ObjFactory,
                ['g'] = _ObjFactory,
                ['N'] = _ObjFactory,
                ['B'] = _ObjFactory,
                ['O'] = _ObjFactory,
                ['╒'] = _ObjFactory,
                ['═'] = _ObjFactory,
                ['╕'] = _ObjFactory,
                ['█'] = _ObjFactory,
            };
        }

        public ObjectOnMap GetObjForChar(char character)
        {
            if (!_availableCharsOfObjs.ContainsKey(character))
            {
                throw new KeyNotFoundException("An object with this symbol cannot be on the map");
            }
            return _availableCharsOfObjs[character].CreateObjectOnMap(character);
        }
    }
}
