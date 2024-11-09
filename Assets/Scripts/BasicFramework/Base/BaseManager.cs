public class BaseManager<T> where T : new()
{
    private static T instance;
    //private BaseManager() { }//��֤�ű������ٴα�ʵ����

    public static T GetInstance()
    {
        if(instance == null)
            instance = new T();
        return instance;
    }
}
