﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using SmartStore.Core.Domain.Customers;
using SmartStore.Core.Security;
using SmartStore.Services.Customers;
using SmartStore.Web.Framework.WebApi;
using SmartStore.Web.Framework.WebApi.OData;
using SmartStore.Web.Framework.WebApi.Security;

namespace SmartStore.WebApi.Controllers.OData
{
    public class CustomerRolesController : WebApiEntityController<CustomerRole, ICustomerService>
	{
		[WebApiQueryable]
		[WebApiAuthenticate(Permission = Permissions.Customer.Role.Read)]
		public IQueryable<CustomerRole> Get()
		{
			return GetEntitySet();
		}

		[WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Customer.Role.Read)]
        public SingleResult<CustomerRole> Get(int key)
		{
			return GetSingleResult(key);
		}

		[WebApiAuthenticate(Permission = Permissions.Customer.Role.Read)]
		public HttpResponseMessage GetProperty(int key, string propertyName)
		{
			return GetPropertyValue(key, propertyName);
		}

		[WebApiAuthenticate(Permission = Permissions.Customer.Role.Create)]
		public IHttpActionResult Post(CustomerRole entity)
		{
			var result = Insert(entity, () => Service.InsertCustomerRole(entity));
			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Customer.Role.Update)]
		public async Task<IHttpActionResult> Put(int key, CustomerRole entity)
		{
			var result = await UpdateAsync(entity, key, () =>
			{
				if (entity != null && entity.IsSystemRole)
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}

				Service.UpdateCustomerRole(entity);
			});

			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Customer.Role.Update)]
		public async Task<IHttpActionResult> Patch(int key, Delta<CustomerRole> model)
		{
			var result = await PartiallyUpdateAsync(key, model, entity =>
			{
				if (entity != null && entity.IsSystemRole)
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}

				Service.UpdateCustomerRole(entity);
			});

			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Customer.Role.Delete)]
		public async Task<IHttpActionResult> Delete(int key)
		{
			var result = await DeleteAsync(key, entity =>
			{
				if (entity != null && entity.IsSystemRole)
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}

				Service.DeleteCustomerRole(entity);
			});

			return result;
		}
	}
}