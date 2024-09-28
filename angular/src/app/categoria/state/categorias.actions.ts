import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Categoria } from "@components/models";

export const CategoriesActions = createActionGroup({
	source: "Categoria",
	events: {
		CategoriaCargada: props<{ categoria: Categoria }>(),
		SearchButtonClicked: emptyProps(),
	},
});
