terraform{

    backend "azurerm"{
        resource_group_name = "AwsLinked-Rg"
        storage_account_name = "scgwvdterraform"
        container_name = "terraform"
    }

    provider "azurerm" {
        version= "=1.38.0"
    }

    resource "azurerm_resource_group" "awsLinkedRg"{
        name = "AwsLinked-Rg"
        location = "Central US"
    }

    resource "azurerm_app_service_plan" "appSvcPlan" {
        name = "scgwvdtestappplan"
        location = azurerm_resource_group.awsLinkedRg.location
        resource_group_name = azurerm_resource_group.awsLinkedRg.name

        sku{
            tier = "Free"
            size = "F1"
        }
    }

    resource "azurerm_app_service" "scgwvdtestapp"{
        name = "scgwvdtestapp"
        location = azurerm_resource_group.awsLinkedRg.location
        resource_group_name = azurerm_resource_group.awsLinkedRg.name
        app_service_plan_id = azurerm_app_service_plan.appSvcPlan.id

        site_config {
            dotnet_framework_version = "v4.0"
            scm_type = "LocalGit"
            use_32_bit_worker_process = true
        }

        app_settings = {
        "SOME_KEY" = "some-value"
        }
    }
}