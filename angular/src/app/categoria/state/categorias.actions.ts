import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Categoria, Item } from "@components/models";
import { HttpErrorResponse } from "@angular/common/http";

export const CategoriesActions = createActionGroup({
	source: "Categoria",
	events: {
		InitCategorias: emptyProps,
		CategoriesObtained: props<{
			categories: Categoria[];
			error?: HttpErrorResponse;
		}>(),
		CategoriaCargada: props<{
			categoria: Categoria;
			error?: HttpErrorResponse;
		}>(),
		SearchInputKeyUp: props<{ value: string }>(),
		SearchButtonClicked: props<{ value: string }>(),
		SearchClearButtonClicked: emptyProps,
		ItemFiltered: props<{ items: Item[] }>(),
	},
});
