(function() {
    'use strict';

    angular
        .module('app')
        .config(translateConfig);

    /* @ngInject */
    function translateConfig(triSettingsProvider, APP_LANGUAGES) {
        var now = new Date();
        // set app name & logo (used in loader, sidemenu, footer, login pages, etc)
        triSettingsProvider.setName('SalesTrooper');
        triSettingsProvider.setCopyright('SalesTrooper TM');
        triSettingsProvider.setLogo('assets/images/logo.png');
        triSettingsProvider.setVersion('2.3.0');

        // setup available languages
        for (var lang = APP_LANGUAGES.length - 1; lang >= 0; lang--) {
            triSettingsProvider.addLanguage({
                name: APP_LANGUAGES[lang].name,
                key: APP_LANGUAGES[lang].key
            });
        }
    }
})();