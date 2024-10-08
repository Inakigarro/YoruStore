import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Categoria, Item } from "@components/models";

export const CategoriesActions = createActionGroup({
	source: "Categoria",
	events: {
		CategoriaCargada: props<{ categoria: Categoria }>(),
		SearchInputKeyUp: props<{ value: string }>(),
		SearchButtonClicked: props<{ value: string }>(),
		SearchClearButtonClicked: emptyProps,
		ItemFiltered: props<{ items: Item[] }>(),
	},
});
