using Roguelike.Engine.ObjectsOnMap.FixedObjects;
using System.Collections.Generic;

namespace Roguelike.Engine.ObjectsOnMap
{
    class CharsOfObjects
    {
        private FixedObjectFactory _fixedObjFactory;
        private Dictionary<char, Factory> _availableCharsOfObjs;

        public CharsOfObjects()
        {
            _fixedObjFactory = new();
            _availableCharsOfObjs = new()
            {
                ['.'] = _fixedObjFactory,
                ['|'] = _fixedObjFactory,
                ['_'] = _fixedObjFactory,
                ['I'] = _fixedObjFactory,
                ['/'] = _fixedObjFactory,
                ['o'] = _fixedObjFactory,

                ['h'] = _fixedObjFactory,
                ['X'] = _fixedObjFactory,
                ['D'] = _fixedObjFactory,
                ['Y'] = _fixedObjFactory,
                ['g'] = _fixedObjFactory,
                ['N'] = _fixedObjFactory,
                ['B'] = _fixedObjFactory,
                ['O'] = _fixedObjFactory,
                ['╒'] = _fixedObjFactory,
                ['═'] = _fixedObjFactory,
                ['╕'] = _fixedObjFactory,
                ['█'] = _fixedObjFactory,
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
