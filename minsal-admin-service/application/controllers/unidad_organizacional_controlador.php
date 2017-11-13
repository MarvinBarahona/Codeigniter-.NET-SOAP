<?php

if(basename($_SERVER['SCRIPT_FILENAME'])==basename(__FILE__))
	exit;

/**
 * @service UnidadesService
 */
	class UnidadesService extends CI_Controller{

		// Constructor
		public function __construct()
    {
			parent::__construct();

			// Cargar el modelo de datos.
			$this->load->model('unidad_organizacional_model');
    }

	/**
	 * Método para mapear el objeto.
	 *
	 * @return UnidadOrganizacional
	 */
	public function getDumpUnidad(){
		// Devuelve un objeto, para que se cree la clase en el cliente.
		return new UnidadOrganizacional();
	}

	/**
	 * Recuperar todas las unidades organizacionales
	 *
	 * @return string
	 */
	public function recuperarUnidades(){
		return json_encode($this->unidad_organizacional_model->get());
	}

	/**
	 * Recuperar una unidad organizacional
	 *
	 * @param int $id
	 * @return string
	 */
	public function recuperarUnidad($id){
		// Recuperar la unidad organizacional.
		$unidad = $this->unidad_organizacional_model->get($id);

		// Si tiene transporte, retornar a sus hijos.
		if($unidad->tiene_transporte)
		{
			$unidad->hijos = $this->unidad_organizacional_model->get(null, $id);
		}

		// Si no tiene tranporte y tiene un padre asignado, recuperarlo.
		else if($unidad->unidad_padre)
		{
			$unidad->padre = $this->unidad_organizacional_model->get($unidad->unidad_padre);
		}

		return json_encode($unidad);
	}

	/**
	 * Crear una unidad
	 *
	 * @param string $nombre
	 * @param string $direccion
	 * @param boolean $transporte
	 * @return string
	 */
	public function crearUnidad($nombre, $departamento, $transporte){
		return json_encode($this->unidad_organizacional_model->create($nombre, $departamento, $transporte));
	}

	/**
	 * Asignar padre
	 *
	 * @param int $id
	 * @param int $id_padre
	 * @return string
	 */
	public function asignarPadre($id, $id_padre){
		// Recuperar los dos objetos involucrados.
		$padre = $this->unidad_organizacional_model->get($id_padre);
		$hija = $this->unidad_organizacional_model->get($id);

		// Si el padre tiene transporte y el hijo no.
		if($padre->tiene_transporte && !$hija->tiene_transporte)
		{
			$r = $this->unidad_organizacional_model->update($id, null, $id_padre);
		}
		// Si no se cumplen los criterios mencionados.
		else
		{
			$r = false;
		}

		return json_encode($r);
	}

	/**
	 * Asignar activa
	 *
	 * @param int $id
	 * @param boolean $activa
	 * @return string
	 */
	public function asignarActiva($id, $activa){
		// Iniciar una transacción.
		$this->unidad_organizacional_model->db->trans_start();

		// Actualizar el registro. Siempre se pierde la asociación con el padre.
		$this->unidad_organizacional_model->update($id, $activa, null);

		// Si es desactivación.
		if($activa === false)
		{
			// Romper la relación con las hijas.
			$hijas = $this->unidad_organizacional_model->get(null, $id);
			foreach ($hijas as $hija)
			{
	    	$this->unidad_organizacional_model->update($hija->id_unidad_organizacional, null, null);
			}
		}

		// Completar la transacción.
		$this->unidad_organizacional_model->db->trans_complete();

		// Retornar el resultado de la transacción. 
		return json_encode($this->unidad_organizacional_model->db->trans_status());
	}
}
