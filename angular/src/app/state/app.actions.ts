import {
	createAction,
	createActionGroup,
	emptyProps,
	props,
} from "@ngrx/store";
import { UserProfile } from "../identity/models";
import { Categoria } from "@root/components/models";
import { HttpErrorResponse } from "@angular/common/http";

export const InitApp = createAction("[App] - Init App");
export const MainToolbarActions = createActionGroup({
	source: "Main Toolbar",
	events: {
		MenuButtonClicked: emptyProps,
		ShoppingCartButtonClicked: emptyProps,
		ProfileButtonClicked: emptyProps,
	},
});

export const backdropClicked = createAction("[Drawer] - Backdrop clicked");

export const SecondaryToolbarActions = createActionGroup({
	source: "Categories Toolbar",
	events: {
		CategoriesObtained: props<{
			categories: Categoria[];
			error?: HttpErrorResponse;
		}>(),
		CategoryButtonClicked: props<{ categoriaId: string }>(),
	},
});

export const userProfileObtained = createAction(
	"[Identity] - User profile obtained",
	props<{ userProfile: UserProfile }>()
);
