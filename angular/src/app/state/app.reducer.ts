import { Action, createReducer, on } from "@ngrx/store";
import {
	backdropClicked,
	categoriesObtained,
	itemCargado,
	SecondaryToolbarActions,
	userProfileObtained,
	MainToolbarActions,
} from "./app.actions";
import { UserProfile } from "../identity/models";
import { Categoria, Item } from "@components/models";
import { ShoppingCartButtonClicked } from "@components/card/state/card.actions";
import { CategoriesActions } from "../categoria/state/categorias.actions";
import { ItemDetailsActions } from "../item-details/state/item-details.actions";

export const APP_STATE_KEY = "app-state";

export interface AppState {
	categories: Categoria[];
	currentUserProfile?: UserProfile;
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
	on(MainToolbarActions.menuButtonClicked, (state) => ({
		...state,
		isMenuOpened: !state.isMenuOpened,
	})),
	on(userProfileObtained, (state, action) => ({
		...state,
		currentUserProfile: action.userProfile,
	})),
	on(SecondaryToolbarActions.categoryButtonClicked, (state) => ({
		...state,
		loading: true,
	})),
	on(CategoriesActions.categoriaCargada, (state, action) => ({
		...state,
		loading: false,
	})),
	on(MainToolbarActions.shoppingCartButtonClicked, (state) => ({
		...state,
		isShoppingCartOpened: !state.isShoppingCartOpened,
	})),
	on(backdropClicked, (state) => ({
		...state,
		isShoppingCartOpened: false,
	})),
	on(
		ShoppingCartButtonClicked,
		ItemDetailsActions.addShoppingCartButtonClicked,
		(state) => ({
			...state,
			shoppingCartCount: state.shoppingCartCount + 1,
		})
	),
	on(itemCargado, (state, action) => ({
		...state,
		currentItem: action.item,
	}))
);

export function reducer(state: AppState, action: Action) {
	return appReducer(state, action);
}
