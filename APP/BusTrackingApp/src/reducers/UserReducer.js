import { actionTypes } from "../actions/UserActions";

export const initialUserSession = {
    isLoading: true,
    userId: null,
    fullName: null,
    email: null,
    roles: null,
    userToken: null
}

// Reducer chỉ là cái mẫu, khuân để update state
const userReducer = (state=initialUserSession, {type,payload}) => {
  switch (type) {
    case actionTypes.RETRIEVE_SESSION:
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
