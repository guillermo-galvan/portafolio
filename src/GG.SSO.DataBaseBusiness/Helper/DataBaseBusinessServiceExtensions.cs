using GG.SSO.DataBaseBusiness;
using GG.SSO.DataBaseBusiness.Business;
using GG.SSO.DataBaseBusiness.Sentences;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class DataBaseBusinessServiceExtensions
    {
        public static IServiceCollection AddDataBaseBusiness(this IServiceCollection services)
        {   
            //Criteria Builder
            services.AddTransient<ICriteriaBuilder, CriteriaBuilder>();            

            //Business
            services.AddScoped<PersistedGrantsBusiness>();
            services.AddScoped<IdentityResourcesBusiness>();
            services.AddScoped<ApiScopesBusiness>();
            services.AddScoped<ApiResourcesBusiness>();
            services.AddScoped<ClientCorsOriginsBusiness>();
            services.AddScoped<ClientsBusiness>();
            services.AddScoped<RolesBusiness>();
            services.AddScoped<AreasBusiness>();
            services.AddScoped<RolesAreasBusiness>();
            services.AddScoped<RoleClaimsBusiness>();
            services.AddScoped<UsersBusiness>();
            services.AddScoped<UsersClientsBusiness>();
            services.AddScoped<ClientsBasicBusiness>();
            services.AddScoped<UserRolesBusiness>();
            services.AddScoped<UserClaimsBusiness>();
            services.AddScoped<UserExternalLoginsBussines>();
            services.AddScoped<UserTokensBusiness>();
            services.AddScoped<CompanyBusiness>();
            services.AddScoped<CompanyAddressesBusiness>();
            services.AddScoped<CompanyContactsBusiness>();
            services.AddScoped<CompanyClientsBusiness>();
            services.AddScoped<CompanyApiResourcesBusiness>();
            services.AddScoped<CompanyApiScopesBusiness>();
            services.AddScoped<CompanyRolesBusiness>();
            services.AddScoped<CompanyAreasBusiness>();
            services.AddScoped<CompanyUsersBusiness>();
            services.AddScoped<ClientPropertiesBusiness>();
            services.AddScoped<ClientIdPRestrictionsBusiness>();
            services.AddScoped<ClientGrantTypesBusiness>();
            services.AddScoped<ClientPostLogoutRedirectUrisBusiness>();
            services.AddScoped<ClientScopesBusiness>();
            services.AddScoped<ClientClaimsBusiness>();
            services.AddScoped<ClientRedirectUrisBusiness>();
            services.AddScoped<ClientSecretsBusiness>();
            services.AddScoped<IdentityResourcesPropertiesBusiness>();
            services.AddScoped<IdentityResourceClaimsBusiness>();
            services.AddScoped<ApiScopeClaimsBusiness>();
            services.AddScoped<ApiScopePropertiesBusiness>();
            services.AddScoped<ApiResourceScopesBusiness>();
            services.AddScoped<ApiResourceSecretsBusiness>();
            services.AddScoped<ApiResourceClaimsBusiness>();
            services.AddScoped<ApiResourcePropertiesBusiness>();
            services.AddScoped<KeysBusiness>();

            //Sentences
            services.AddScoped<PersistedGrantsSentences>();
            services.AddScoped<IdentityResourcesSentences>();
            services.AddScoped<ClientsSentences>();
            services.AddScoped<ClientCorsOriginsSentences>();
            services.AddScoped<ClientPropertiesSentences>();
            services.AddScoped<ClientIdPRestrictionsSentences>();
            services.AddScoped<ClientGrantTypesSentences>();
            services.AddScoped<ClientPostLogoutRedirectUrisSentences>();
            services.AddScoped<ClientScopesSentences>();
            services.AddScoped<ClientClaimsSentences>();
            services.AddScoped<ClientRedirectUrisSentences>();
            services.AddScoped<ClientSecretsSentences>();
            services.AddScoped<ApiScopesSentences>();
            services.AddScoped<ApiScopeClaimsSentences>();
            services.AddScoped<ApiScopePropertiesSentences>();
            services.AddScoped<ApiResourcesSentences>();
            services.AddScoped<ApiResourceScopesSentences>();
            services.AddScoped<ApiResourceSecretsSentences>();
            services.AddScoped<ApiResourceClaimsSentences>();
            services.AddScoped<ApiResourcePropertiesSentences>();
            services.AddScoped<RolesSentences>();
            services.AddScoped<AreasSentences>();
            services.AddScoped<RolesAreasSentences>();
            services.AddScoped<RoleClaimsSentences>();
            services.AddScoped<UsersSentences>();
            services.AddScoped<UsersClientsSentences>();
            services.AddScoped<ClientsBasicSentences>();
            services.AddScoped<UserRolesSentencias>();
            services.AddScoped<UserClaimsSentences>();
            services.AddScoped<UserExternalLoginsSentences>();
            services.AddScoped<UserTokensSentences>();
            services.AddScoped<CompanySentences>();
            services.AddScoped<CompanyAddressesSentences>();
            services.AddScoped<CompanyContactsSentences>();
            services.AddScoped<CompanyClientsSentences>();
            services.AddScoped<CompanyApiResourcesSentences>();
            services.AddScoped<CompanyApiScopesSentences>();
            services.AddScoped<CompanyRolesSentences>();
            services.AddScoped<CompanyAreasSentences>();
            services.AddScoped<CompanyUsersSentences>();
            services.AddScoped<IdentityResourcesPropertiesSentences>();
            services.AddScoped<IdentityResourceClaimsSentences>();
            services.AddScoped<KeysSentences>();

            return services;
        }

    }
}
