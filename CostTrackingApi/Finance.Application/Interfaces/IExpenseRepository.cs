﻿using Finance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Interfaces
{
    public interface IExpenseRepository
    {
        Task<List<ExpenseItem>> GetItemsByExpenseId(int id);
    }
}
