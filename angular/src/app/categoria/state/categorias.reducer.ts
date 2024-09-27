import { Categoria, Item } from "@components/models";
import { Action, createReducer, on } from "@ngrx/store";
import { CategoriesActions } from "./categorias.actions";
import { SecondaryToolbarActions } from "@root/app/state/app.actions";

export const CATEGORIES_FEATURE_KEY = "categorias";
export interface CategoriesState {
	currentCategory?: Categoria;
	items?: Item[];
	loaded: boolean;
}
export interface CategoriesPartialState {
	readonly [CATEGORIES_FEATURE_KEY]: CategoriesState;
}
const initialState: CategoriesState = {
	loaded: false,
};
export const categoriesReducer = createReducer(
	initialState,
	on(SecondaryToolbarActions.categoryButtonClicked, (state) => ({
		...state,
		currentCategory: undefined,
		items: undefined,
		loaded: false,
	})),
	on(CategoriesActions.categoriaCargada, (state, action) => ({
		...state,
		currentCategory: action.categoria,
		items: action.categoria.items,
		loaded: true,
	}))
);
export function reducer(state: CategoriesState, action: Action) {
	return categoriesReducer(state, action);
}
