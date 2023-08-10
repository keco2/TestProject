using System;
using System.ServiceModel;
using Unity;

namespace TaskMgmt.Server
{
    public class UnityServiceHost : ServiceHost
    {

        private IUnityContainer unityContainer;

        public UnityServiceHost(IUnityContainer unityContainer, Type serviceType)
            : base(serviceType)
        {
            this.unityContainer = unityContainer;
        }

        protected override void OnOpening()
        {
            base.OnOpening();

            if (this.Description.Behaviors.Find<UnityServiceBehavior>() == null)
            {
                this.Description.Behaviors.Add(new UnityServiceBehavior(this.unityContainer));
            }
        }
    }
}
