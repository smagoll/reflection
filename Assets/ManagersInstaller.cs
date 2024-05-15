using UnityEngine;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private LevelMaster levelMaster;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
        Container.Bind<LevelMaster>().FromInstance(levelMaster).AsSingle();
    }
}
