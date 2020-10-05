import { actionTypes } from "../actions/UserActions";

export const initialLoginState = {
    isLoading: true,
    userName: null,
    userToken: null
}

// Reducer chỉ là cái mẫu, khuân để update state
const userReducer = (state=initialLoginState, {type,payload}) => {
  switch (type) {
    case actionTypes.RETRIEVE_TOKEN:
      return {
        ...state,
        ...payload.user,
        isLoading: true
      };
    case actionTypes.LOGIN:
      return {
        ...state,
        ...payload.user,
        isLoading: false,
      };
    case actionTypes.LOGOUT:
      return {
        ...state,
        userName: null,
        userToken: null,
        isLoading: false,
      };
    default:
      return state 
  }
};

export default userReducer;
