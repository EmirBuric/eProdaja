import 'package:eprodaja_mobile/models/proizvod.dart';
import 'package:eprodaja_mobile/models/search_result.dart';
import 'package:eprodaja_mobile/providers/product_provider.dart';

class LoggedProductProvider extends ProductProvider {
  @override
  Future<SearchResult<Proizvod>> get({filter}) {
    print("Im in logged product provider");

    return super.get(filter: filter);
  }
}
