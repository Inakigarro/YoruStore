import { createActionGroup, props } from "@ngrx/store";
import { Categoria } from "@components/models";

export const CategoriesActions = createActionGroup({
	source: "Categoria",
	events: {
		CategoriaCargada: props<{ categoria: Categoria }>(),
	},
});
