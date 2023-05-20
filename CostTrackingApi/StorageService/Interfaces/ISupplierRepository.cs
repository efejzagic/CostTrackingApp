﻿using StorageService.Models;

namespace StorageService.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetById(int id);

        Task<Supplier> Create (Supplier supplier);

        Task<Supplier> Edit(Supplier supplier);
        Task<bool> Delete(int id);
        Task<bool> SoftDelete (int id);

    }
}
