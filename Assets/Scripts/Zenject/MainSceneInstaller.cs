using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{   
    [SerializeField] private Shop shop;

    public override void InstallBindings()
    {
        Container.Bind<Wallet>().FromNew().AsSingle();
        Container.Bind<OpenSkinsChecker>().FromNew().AsSingle();
        Container.Bind<SelectedSkinsChecker>().FromNew().AsSingle();
        Container.Bind<SkinSelector>().FromNew().AsSingle();
        Container.Bind<SkinUnlocker>().FromNew().AsSingle();
    }
}
