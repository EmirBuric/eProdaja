import 'package:json_annotation/json_annotation.dart';

part 'proizvod.g.dart'; // Ensure this matches exactly with the intended generated file name.

@JsonSerializable()
class Proizvod {
  int? proizvodId;
  String? naziv;
  String? slika;
  String? sifra;
  double? cijena;
  int? vrstaId;
  int? jedinicaMjereId;

  Proizvod({this.proizvodId, this.naziv});

  factory Proizvod.fromJson(Map<String, dynamic> json) =>
      _$ProizvodFromJson(json);
  Map<String, dynamic> toJson() => _$ProizvodToJson(this);
}
