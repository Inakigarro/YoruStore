import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

export const routes: Routes = [
	{
		path: ":itemId",
		loadChildren: () =>
			import("./item-details/item-details.module").then(
				(m) => m.ItemDetailsModule
			),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
