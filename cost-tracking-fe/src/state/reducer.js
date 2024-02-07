import { combineReducers } from "redux";
import entitiesReducer from "./entities";
import bugsReducer from "./bugs";
import suppliersReducer from "./suppliers";

export default combineReducers({
//   entities: entitiesReducer
  bugs: bugsReducer,
  suppliers: suppliersReducer
});
