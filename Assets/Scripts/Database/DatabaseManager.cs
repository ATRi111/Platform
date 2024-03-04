using Services;
using System;

public class DatabaseManager : Service,IService
{
    public override Type RegisterType => GetType();
}
