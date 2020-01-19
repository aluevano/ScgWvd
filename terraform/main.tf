
provider "azurerm" {
    version= "=1.38.0"
}

resource "azurerm_resource_group" "testRg"{
    name = "testrg"
    location = "Central US"
}

resource "azurerm_app_service_plan" "testAppSvcPlan" {
    name = "test-appsvcplan"
    location = "${azurerm_resource_group.testRg.location}"
    resource_group_name = "${azurerm_resource_group.testRg.name}"

    sku{
        tier = "Free"
        size = "F1"
    }
}

resource "azurerm_app_service" "testAppSvc"{
    name = "example-app-svc"
    location = "${azurerm_resource_group.testRg.location}"
    resource_group_name = "${azurerm_resource_group.testRg.name}"
    app_service_plan_id = "${azurerm_app_service_plan.testAppSvcPlan.id}"

    site_config {
        dotnet_framework_version = "v4.7"
        scm_type = "LocalGit"
    }

    app_settings = {
    "SOME_KEY" = "some-value"
    }
}