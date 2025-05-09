using AI;
using Configurations;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyConfiguration _enemyConfiguration;
        [SerializeField] private int _initialPoolSize = 10;

        public override void InstallBindings()
        {
            // pool
            Container.BindMemoryPool<Enemy, Enemy.Pool>()
                .WithInitialSize(_initialPoolSize)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransformGroup("Enemies").AsSingle();
                
            // factory
            //Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>()
                .FromMethod(context => new EnemyFactory(
                    context.Container.Resolve<Enemy.Pool>(),
                    _enemyConfiguration)).AsSingle();
        }
    }
}