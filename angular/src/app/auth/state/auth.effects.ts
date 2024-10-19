import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { AuthService } from "../auth.service";
import { AuthActions } from "./auth.actions";
import { tap } from "rxjs";

@Injectable()
export class AuthEffects {
	public navigateToLoginPage$ = createEffect(
		() =>
			this.actions$.pipe(
				ofType(AuthActions.userNotLoggedIn),
				tap(() => this.authService.navigate(["login"]))
			),
		{ dispatch: false }
	);
	constructor(
		private actions$: Actions,
		private readonly authService: AuthService
	) {}
}
