import { createFeatureSelector, createSelector } from "@ngrx/store";
import { AUTH_FEATURE_KEY, AuthState } from "./auth.reducer";

export const getAuthState = createFeatureSelector<AuthState>(AUTH_FEATURE_KEY);

export const getLoggedIn = createSelector(
	getAuthState,
	(state) => state.isLoggedIn
);

export const getToken = createSelector(
	getAuthState,
	(state) => state.token ?? ""
);
