﻿using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using SmartStore.Core.Domain.Catalog;
using SmartStore.Core.Security;
using SmartStore.Services.Catalog;
using SmartStore.Web.Framework.WebApi;
using SmartStore.Web.Framework.WebApi.OData;
using SmartStore.Web.Framework.WebApi.Security;

namespace SmartStore.WebApi.Controllers.OData
{
    public class ProductVariantAttributesController : WebApiEntityController<ProductVariantAttribute, IProductAttributeService>
	{
		[WebApiQueryable]
		[WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
		public IQueryable<ProductVariantAttribute> Get()
		{
			return GetEntitySet();
		}

		[WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public SingleResult<ProductVariantAttribute> Get(int key)
		{
			return GetSingleResult(key);
		}

		[WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
		public HttpResponseMessage GetProperty(int key, string propertyName)
		{
			return GetPropertyValue(key, propertyName);
		}

		[WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditVariant)]
		public IHttpActionResult Post(ProductVariantAttribute entity)
		{
			var result = Insert(entity, () => Service.InsertProductVariantAttribute(entity));
			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditVariant)]
		public async Task<IHttpActionResult> Put(int key, ProductVariantAttribute entity)
		{
			var result = await UpdateAsync(entity, key, () => Service.UpdateProductVariantAttribute(entity));
			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditVariant)]
		public async Task<IHttpActionResult> Patch(int key, Delta<ProductVariantAttribute> model)
		{
			var result = await PartiallyUpdateAsync(key, model, entity => Service.UpdateProductVariantAttribute(entity));
			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditVariant)]
		public async Task<IHttpActionResult> Delete(int key)
		{
			var result = await DeleteAsync(key, entity => Service.DeleteProductVariantAttribute(entity));
			return result;
		}

		#region Navigation properties

		[WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public SingleResult<ProductAttribute> GetProductAttribute(int key)
		{
			return GetRelatedEntity(key, x => x.ProductAttribute);
		}

		[WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public IQueryable<ProductVariantAttributeValue> GetProductVariantAttributeValues(int key)
		{
			return GetRelatedCollection(key, x => x.ProductVariantAttributeValues);
		}

		#endregion
	}
}
