using BrokerMicroservice.Application.Models.Base;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservise.ValueObgect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservice.Application.Models.Asset
{
    public record class AssetModel(Guid Id, AssetType AssetType, int MinimalUnit, decimal PurchasePrice, Guid BrokerId) : IModel<Guid>;
}
