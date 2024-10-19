import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Login } from "@root/components/models";

export const AuthActions = createActionGroup({
	source: "Auth",
	events: {
		LogInButtonClicked: props<{ login: Login }>(),
		UserNotLoggedIn: emptyProps(),
	},
});
