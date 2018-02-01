define(['plugins/router', 'durandal/app', 'durandal/system'], function (router, app, system) {


    return {
        router: router,
        activate: function () {
            router.guardRoute = function (instance, instruction) {
                if ((sessionStorage.getItem("username") == null) && instruction.config.route !== "" && instruction.config.route !== "signup" && instruction.config.route !== "usageStats") {

                    return false;
                }
                else {
                    return true;
                }
            }
            router.map([
                { route: '', title: 'Login', moduleId: 'viewmodels/login', nav: true },
                { route: 'about', title: 'About', moduleId: 'viewmodels/about', nav: true },
                { route: 'profile', title: 'Profile', moduleId: 'viewmodels/profile', nav: true },
                { route: 'signup', title: 'Signup', moduleId: 'viewmodels/signup', nav: true },
                { route: 'welcome', title: 'Welcome', moduleId: 'viewmodels/welcome', nav: true },
                { route: 'usage', title: 'usage', moduleId: 'viewmodels/usageStats', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});