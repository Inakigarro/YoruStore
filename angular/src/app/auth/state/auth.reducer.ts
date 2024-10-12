import { Action, createReducer } from "@ngrx/store";

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

export const authReducer = createReducer(initialState);

export function reducer(state: AuthState, action: Action) {
	return reducer(state, action);
}
