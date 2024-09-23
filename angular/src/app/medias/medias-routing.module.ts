import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MediasListComponent } from "./list/medias-list.component";
import { MediasRoutingComponent } from "./medias-routing.component";

const routes: Routes = [
	{
		path: "",
		component: MediasRoutingComponent,
		children: [
			{
				path: "",
				component: MediasListComponent,
			},
			{
				path: ":itemId",
				loadChildren: () =>
					import("../item-details/item-details.module").then(
						(m) => m.ItemDetailsModule
					),
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class MediasRoutingModule {}
