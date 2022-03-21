namespace Roguelike.Engine.ObjectsOnMap
{
    /// <summary>
    /// Если FixedObject реализует этот интерфейс, то при создании карты будет создан отдельный экземпляр
    /// на каждом месте, где присутствует этот объект. Другие интерфейсы, диктующие способы взаимодействия
    /// с FixedObject, должны наследовать от него.
    /// </summary>
    public interface IChangeAble
    {
    }
}
