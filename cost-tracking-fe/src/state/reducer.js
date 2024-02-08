import { combineReducers } from "redux";
import entitiesReducer from "./entities";
import suppliersReducer from "./suppliers";
import articlesReducer from "./articles";
import ordersReducer from "./orders";

export default combineReducers({
  //   entities: entitiesReducer
  suppliers: suppliersReducer,
  articles: articlesReducer,
  orders: ordersReducer
});
