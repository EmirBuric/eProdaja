
import 'package:eprodaja_mobile/models/proizvod.dart';
import 'package:eprodaja_mobile/providers/base_provider.dart';

class ProductProvider extends BaseProvider<Proizvod> {
  ProductProvider() : super("Proizvodi");

  @override
  Proizvod fromJson(data) {
    // TODO: implement fromJson
    return Proizvod.fromJson(data);
  }
}
