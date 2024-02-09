import { createSlice } from "@reduxjs/toolkit";
import { createSelector } from "reselect";
import axios from "axios";
import { apiCallBegan } from "./api";
import moment from "moment";

const slice = createSlice({
  name: "suppliers",
  initialState: {
    list: [],
    loading: false,
    lastFetch: null
  },
  reducers: {
    suppliersRequested: (suppliers, action) => {
      suppliers.loading = true;
    },

    suppliersReceived: (suppliers, action) => {
      suppliers.list = action.payload.data;
      suppliers.loading = false;
      suppliers.lastFetch = Date.now();
    },

    suppliersRequestFailed: (suppliers, action) => {
      suppliers.loading = false;
    },

    supplierAdded: (suppliers, action) => {
      console.log("Add payload", action.payload);
      suppliers.list.push(action.payload.data);
    },

    supplierEdited: (suppliers, action) => {
      console.log("Edit payload", action.payload);
      const index = suppliers.list.findIndex(
        (supplier) => supplier.id === action.payload.data.id
      );
      suppliers.list[index] = action.payload.data;
    },

    supplierDeleted: (suppliers, action) => {
      console.log("action.payload", action.payload);
      return {
        ...suppliers,
        list: suppliers.list.filter(
          (supplier) =>
            supplier && supplier.id !== parseInt(action.payload.message)
        )
      };
    }
  }
});

export const {
  supplierAdded,
  supplierDeleted,
  supplierEdited,
  suppliersReceived,
  suppliersRequested,
  suppliersRequestFailed
} = slice.actions;
export default slice.reducer;

// Action Creators
const url = "/Supplier";
const token = localStorage.getItem("accessToken");
export const loadsuppliers = () => (dispatch, getState) => {
  const { lastFetch } = getState().suppliers;

  const diffInMinutes = moment().diff(moment(lastFetch), "minutes");
  if (diffInMinutes < 1) return;

  return dispatch(
    apiCallBegan({
      url,
      onStart: suppliersRequested.type,
      onSuccess: suppliersReceived.type,
      onError: suppliersRequestFailed.type,
      token
    })
  );
};

export const addsupplier = (supplier) =>
  apiCallBegan({
    url,
    method: "post",
    data: { Value: supplier },
    onSuccess: supplierAdded.type,
    token
  });

export const editSupplier = (supplier) =>
  apiCallBegan({
    url,
    method: "put",
    data: { Value: supplier },
    onSuccess: supplierEdited.type,
    token
  });

export const deleteSupplier = (supplierId) =>
  apiCallBegan({
    url: url + "/" + supplierId,
    method: "delete",
    data: supplierId,
    onSuccess: supplierDeleted.type,
    token
  });

// Selector
// Memoization

export const getsuppliersByUser = (userId) =>
  createSelector(
    (state) => state.entities.suppliers,
    (suppliers) => suppliers.filter((supplier) => supplier.userId === userId)
  );

export const selectSuppliers = createSelector(
  (state) => state.suppliers,
  (suppliers) => suppliers.list
);

export const selectSuppliersLoading = createSelector(
  (state) => state.suppliers,
  (suppliers) => suppliers.loading
);

export const selectSupplier = (supplierId) => {
  console.log("Supplier id", supplierId);
  const selector = createSelector(
    (state) => state.suppliers,
    (suppliers) =>
      suppliers.list.find((supplier) => supplier.id === Number(supplierId))
  );
  return (state) => {
    const selectedSupplier = selector(state);
    console.log("Selected supplier", selectedSupplier);
    return selectedSupplier;
  };
};

export const selectArticleBySupplierId = (supplierId) => {
  console.log("Supplier id", supplierId);
  const selector = createSelector(
    (state) => state.suppliers,
    (suppliers) =>
      suppliers.list.find((supplier) => supplier.id === Number(supplierId))
        .articles
  );
  return (state) => {
    const selectedSupplier = selector(state);
    console.log("Selected article", selectedSupplier);
    return selectedSupplier;
  };
};
