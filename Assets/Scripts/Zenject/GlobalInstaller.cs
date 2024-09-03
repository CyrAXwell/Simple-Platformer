using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPersistentData>().To<PersistentData>().FromNew().AsSingle();
        Container.Bind<IDataProvider>().To<DataLocalProvider>().FromNew().AsSingle().NonLazy();
        Container.Bind<Wallet>().FromNew().AsSingle();
    }
}
