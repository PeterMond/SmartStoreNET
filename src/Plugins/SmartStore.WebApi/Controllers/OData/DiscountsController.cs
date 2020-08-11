﻿using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using SmartStore.Core.Domain.Catalog;
using SmartStore.Core.Domain.Discounts;
using SmartStore.Core.Security;
using SmartStore.Services.Discounts;
using SmartStore.Web.Framework.WebApi;
using SmartStore.Web.Framework.WebApi.OData;
using SmartStore.Web.Framework.WebApi.Security;

namespace SmartStore.WebApi.Controllers.OData
{
    public class DiscountsController : WebApiEntityController<Discount, IDiscountService>
	{
        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Promotion.Discount.Read)]
        public IQueryable<Discount> Get()
        {
            return GetEntitySet();
        }

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Promotion.Discount.Read)]
        public SingleResult<Discount> Get(int key)
		{
			return GetSingleResult(key);
		}

		[WebApiAuthenticate(Permission = Permissions.Promotion.Discount.Read)]
		public HttpResponseMessage GetProperty(int key, string propertyName)
		{
			return GetPropertyValue(key, propertyName);
		}

		[WebApiAuthenticate(Permission = Permissions.Promotion.Discount.Create)]
		public IHttpActionResult Post(Discount entity)
		{
			var result = Insert(entity, () => Service.InsertDiscount(entity));
			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Promotion.Discount.Update)]
		public async Task<IHttpActionResult> Put(int key, Discount entity)
		{
			var result = await UpdateAsync(entity, key, () => Service.UpdateDiscount(entity));
			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Promotion.Discount.Update)]
		public async Task<IHttpActionResult> Patch(int key, Delta<Discount> model)
		{
			var result = await PartiallyUpdateAsync(key, model, entity => Service.UpdateDiscount(entity));
			return result;
		}

		[WebApiAuthenticate(Permission = Permissions.Promotion.Discount.Delete)]
		public async Task<IHttpActionResult> Delete(int key)
		{
			var result = await DeleteAsync(key, entity => Service.DeleteDiscount(entity));
			return result;
		}

		#region Navigation properties

		[WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Category.Read)]
        public IQueryable<Category> GetAppliedToCategories(int key)
		{
			return GetRelatedCollection(key, x => x.AppliedToCategories);
		}

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Manufacturer.Read)]
        public IQueryable<Manufacturer> GetAppliedToManufacturers(int key)
        {
            return GetRelatedCollection(key, x => x.AppliedToManufacturers);
        }

        [WebApiQueryable]
        [WebApiAuthenticate(Permission = Permissions.Catalog.Product.Read)]
        public IQueryable<Product> GetAppliedToProducts(int key)
        {
            return GetRelatedCollection(key, x => x.AppliedToProducts);
        }

        #endregion
    }
}
