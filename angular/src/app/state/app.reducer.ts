import { Action, createReducer, on } from "@ngrx/store";
import {
	backdropClicked,
	categoriesObtained,
	itemCargado,
	menuButtonClicked,
	SecondaryToolbarActions,
	shoppingCartButtonClicked,
	userProfileObtained,
} from "./app.actions";
import { UserProfile } from "../identity/models";
import { Categoria, Item } from "@components/models";
import {
	DetailsButtonClicked,
	ShoppingCartButtonClicked,
} from "@components/card/state/card.actions";

export const APP_STATE_KEY = "app-state";

export interface AppState {
	categories: Categoria[];
	currentUserProfile?: UserProfile;
	currentCategory?: Categoria;
	currentItem?: Item;
	isMenuOpened: boolean;
	isShoppingCartOpened: boolean;
	shoppingCartCount: number;
	loading: boolean;
}

export interface AppPartialState {
	readonly [APP_STATE_KEY]: AppState;
}

const initialState: AppState = {
	categories: [],
	isMenuOpened: true,
	isShoppingCartOpened: false,
	shoppingCartCount: 0,
	loading: false,
};

export const appReducer = createReducer(
	initialState,
	on(categoriesObtained, (state, action) => ({
		...state,
		categories: action.categorias,
	})),
	on(menuButtonClicked, (state) => ({
		...state,
		isMenuOpened: !state.isMenuOpened,
	})),
	on(userProfileObtained, (state, action) => ({
		...state,
		currentUserProfile: action.userProfile,
	})),
	on(SecondaryToolbarActions.buttonClicked, (state, action) => ({
		...state,
		currentCategory: undefined,
		loading: true,
	})),
	on(SecondaryToolbarActions.categoriaCargada, (state, action) => ({
		...state,
		currentCategory: action.categoria,
		loading: false,
	})),
	on(shoppingCartButtonClicked, (state) => ({
		...state,
		isShoppingCartOpened: !state.isShoppingCartOpened,
	})),
	on(backdropClicked, (state) => ({
		...state,
		isShoppingCartOpened: false,
	})),
	on(ShoppingCartButtonClicked, (state) => ({
		...state,
		shoppingCartCount: state.shoppingCartCount + 1,
	})),
	on(itemCargado, (state, action) => ({
		...state,
		currentItem: action.item,
	}))
);

export function reducer(state: AppState, action: Action) {
	return appReducer(state, action);
}
