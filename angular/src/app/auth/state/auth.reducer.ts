import { createReducer, on } from "@ngrx/store";
import { AuthActions } from "./auth.actions";

export const AUTH_FEATURE_KEY = "auth";

export interface AuthState {
	isLoggedIn: boolean;
	token?: string;
}

export interface AuthPartialState {
	readonly [AUTH_FEATURE_KEY]: AuthState;
}

export const initialState: AuthState = {
	isLoggedIn: false,
};

export const authReducer = createReducer(
	initialState,
	on(AuthActions.userLoggedIn, (state, action) => ({
		...state,
		isLoggedIn: true,
		token: action.response.token,
	}))
);
