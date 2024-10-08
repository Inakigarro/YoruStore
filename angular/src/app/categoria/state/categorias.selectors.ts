import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CATEGORIES_FEATURE_KEY, CategoriesState } from "./categorias.reducer";

export const getCategoriesState = createFeatureSelector<CategoriesState>(
	CATEGORIES_FEATURE_KEY
);

export const getLoaded = createSelector(
	getCategoriesState,
	(state) => state.loaded
);

export const getCurrentCategory = createSelector(
	getCategoriesState,
	(state) => state.currentCategory
);

export const getCurrentCategoryName = createSelector(
	getCurrentCategory,
	(cat) => (cat ? cat.nombre : "")
);

export const getCurrentCategoryItems = createSelector(
	getCurrentCategory,
	(cat) => (cat ? cat.items : [])
);

export const getCurrentItems = createSelector(
	getCategoriesState,
	(state) => state.items ?? []
);
