// https://c4model.com/
// https://structurizr.com/
// https://github.com/structurizr/dsl/blob/master/docs/language-reference.md

workspace {

    model {
        user = person "User"

        stockCube = softwareSystem "Stock Cube" "" "" {
            blazorServer = container "Stock Cube Blazor Server" "" "Blazor Server" {
            }
            database = container "Database" "" "MS Sql Server" {
              sectionsTable = component "Sections" "" "Table" ""
            }
            kitchenAPI = container "Stock Cube Kitchen API" "" "ASP.Net WebAPI" {
              getSections = component "Get Sections" "/api/v1/section" "API Endpoint" ""
              getSectionById = component "Get Section by Id" "/api/v1/section/{SectionId}" "API Endpoint" ""
            }
        }
        user -> blazorServer "Uses"
        blazorServer -> kitchenAPI "Uses" "http / Rest"
        getSections -> sectionsTable "Get all sections" "Ado.net"
    }

    views {
        systemContext stockCube {
            include *
            autolayout lr
        }

        container stockCube {
            include *
            autolayout lr
        }

        component blazorServer {
            include *
            autolayout lr
        }

        component database {
            include *
            autolayout lr
        }

        component kitchenAPI {
            include *
            autolayout lr
        }
        theme default
    }

}
