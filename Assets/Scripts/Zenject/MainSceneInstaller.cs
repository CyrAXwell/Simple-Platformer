using Zenject;

public class MainSceneInstaller : MonoInstaller
{   
    public override void InstallBindings()
    {
        Container.Bind<OpenSkinsChecker>().FromNew().AsSingle();
        Container.Bind<SelectedSkinsChecker>().FromNew().AsSingle();
        Container.Bind<SkinSelector>().FromNew().AsSingle();
        Container.Bind<SkinUnlocker>().FromNew().AsSingle();
    }
}
