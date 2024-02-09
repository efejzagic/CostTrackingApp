import { createSlice } from "@reduxjs/toolkit";
import { createSelector } from "reselect";
import { apiCallBegan } from "./api";
import moment from "moment";

const slice = createSlice({
  name: "orders",
  initialState: {
    list: [],
    loading: false,
    lastFetch: null
  },
  reducers: {
    ordersRequested: (orders, action) => {
      orders.loading = true;
    },

    ordersReceived: (orders, action) => {
      orders.list = action.payload.data;
      orders.loading = false;
      orders.lastFetch = Date.now();
    },

    ordersRequestFailed: (orders, action) => {
      orders.loading = false;
    },

    orderAdded: (orders, action) => {
      console.log("Add payload", action.payload);
      orders.list.push(action.payload.data);
    },

    orderDeleted: (orders, action) => {
      console.log("action.payload", action.payload);
      return {
        ...orders,
        list: orders.list.filter(
          (order) => order && order.id !== parseInt(action.payload.message)
        )
      };
    }
  }
});

export const {
  orderAdded,
  orderDeleted,
  ordersReceived,
  ordersRequested,
  ordersRequestFailed
} = slice.actions;
export default slice.reducer;

// Action Creators
const url = "/order";
const token = localStorage.getItem("accessToken");
export const loadOrders = () => (dispatch, getState) => {
  const { lastFetch } = getState().orders;

  const diffInMinutes = moment().diff(moment(lastFetch), "minutes");
  if (diffInMinutes < 1) return;

  return dispatch(
    apiCallBegan({
      url,
      onStart: ordersRequested.type,
      onSuccess: ordersReceived.type,
      onError: ordersRequestFailed.type,
      token
    })
  );
};

export const addOrder = (order) => {
  console.log({ order });
  apiCallBegan({
    url,
    method: "post",
    data: order,
    onSuccess: orderAdded.type,
    token
  });
};

export const deleteOrder = (orderId) =>
  apiCallBegan({
    url: url + "/" + orderId,
    method: "delete",
    data: orderId,
    onSuccess: orderDeleted.type,
    token
  });

// Selector
// Memoization

export const getOrdersByUser = (userId) =>
  createSelector(
    (state) => state.entities.orders,
    (orders) => orders.filter((order) => order.userId === userId)
  );

export const selectOrders = createSelector(
  (state) => state.orders,
  (orders) => orders.list
);

export const selectOrdersLoading = createSelector(
  (state) => state.orders,
  (orders) => orders.loading
);

export const selectOrder = (orderId) => {
  console.log("order id", orderId);
  const selector = createSelector(
    (state) => state.orders,
    (orders) => orders.list.find((order) => order.id === Number(orderId))
  );
  return (state) => {
    const selectedorder = selector(state);
    console.log("Selected order", selectedorder);
    return selectedorder;
  };
};
