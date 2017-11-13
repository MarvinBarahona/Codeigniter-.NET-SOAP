<?php

if(basename($_SERVER['SCRIPT_FILENAME'])==basename(__FILE__))
	exit;

/**
 * @pw_element int $id_unidad_organizacional
 * @pw_element int $unidad_padre
 * @pw_element string $nombre
 * @pw_element string $departamento
 * @pw_element boolean $activa
 * @pw_element boolean $tiene_transporte
 * @pw_element UnidadOrganizacional $padre
 * @pw_element UnidadOrganizacionalArray $hijos
 * @pw_complex UnidadOrganizacional
 */
class UnidadOrganizacional{
	public $id_unidad_transporte = 1;
	public $unidad_padre = 1;
	public $nombre = "nom";
	public $departamento = "ss";
	public $activa = true;
	public $tiene_transporte = false;
}

/**
 * @pw_complex UnidadOrganizacionalArray
 */
