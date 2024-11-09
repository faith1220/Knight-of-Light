public class BaseManager<T> where T : new()
{
    private static T instance;
    //private BaseManager() { }//保证脚本不会再次被实例化

    public static T GetInstance()
    {
        if(instance == null)
            instance = new T();
        return instance;
    }
}
