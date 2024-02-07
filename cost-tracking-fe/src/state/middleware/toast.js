import { toast as toastMessage } from "react-toastify";

const toast = (store) => (next) => (action) => {
  console.log("action type toast", action.type);
  if (action.type === "suppliers/suppliersRequestFailed") {
    console.log("Toastify", action.payload.message);
    toastMessage.error(action.payload);
  } else if (action.type === "suppliers/supplierAdded") {
    console.log("Toastify", action.payload.message);
    toastMessage.success("Supplier created!");
  }
  return next(action);
};

export default toast;
