using Autofac;
using Core;
using SampleApplication.AppServices;
using SampleApplication.Views;

namespace SampleApplication
{
    public class IocApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Repository>().As<IRepository>().AsSelf().SingleInstance();

            builder.RegisterType<ShareService>().As<IShareService>().AsSelf();

            builder.RegisterType<ContactValidator>().As<IModelValidator<Contact>>().AsSelf();

            builder.RegisterType<MainViewModel>().Keyed<IViewModel>(Constants.Navigation.MainPage);
            builder.RegisterType<ContactViewModel>().Keyed<IViewModel>(Constants.Navigation.ContactPage);
            builder.RegisterType<FirstContactPromptViewModel>().Keyed<IViewModel>(Constants.Navigation.FirstContactPromptPage);
            builder.RegisterType<AuthViewModel>().Keyed<IViewModel>(Constants.Navigation.AuthPage);
            builder.RegisterType<RegisterViewModel>().Keyed<IViewModel>(Constants.Navigation.RegisterPage);
            builder.RegisterType<WelcomeViewModel>().Keyed<IViewModel>(Constants.Navigation.WelcomePage);

            builder.RegisterType<MainPage>().Keyed<IView>(Constants.Navigation.MainPage);
            builder.RegisterType<WelcomePage>().Keyed<IView>(Constants.Navigation.WelcomePage);
            builder.RegisterType<AuthPage>().Keyed<IView>(Constants.Navigation.AuthPage);
            builder.RegisterType<RegisterPage>().Keyed<IView>(Constants.Navigation.RegisterPage);
            builder.RegisterType<FirstContactPromptPage>().Keyed<IView>(Constants.Navigation.FirstContactPromptPage);
            builder.RegisterType(typeof(ContactPage)).Keyed(Constants.Navigation.ContactPage, typeof(IView));
        }
    }
}