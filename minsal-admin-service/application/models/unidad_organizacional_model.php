<?php
class unidad_organizacional_model extends CI_Model {

    public function __construct()
    {
        // Cargar la base de datos.
        $this->load->database();
    }

    public function get($id = null, $id_padre = null)
    {
        // Elegir los campos a recuperar.
        $this->db->select('id_unidad_organizacional, unidad_padre, nombre, departamento, tiene_transporte, activa');

        // Si no hay parámetros, recupera todos.
        if ($id === null && $id_padre === null)
        {
            $query = $this->db->get('unidad_organizacional');
            $resultado = $query->result();
        }

        // Si no hay padre, recupera un registro individual.
        else if($id_padre === null)
        {
          $query = $this->db->get_where('unidad_organizacional', array('id_unidad_organizacional' => $id));
          $resultado = $query->row();
        }

        // Si hay padre, recupera las hijas.
        else
        {
          $query = $this->db->get_where('unidad_organizacional', array('unidad_padre' => $id_padre));
          $resultado = $query->result();
        }

        // Lo siguiente se usa para convertir los campos booleanos, ya que postgres por defecto retorna "t" y "f".

        // Si el resultado es un arreglo.
        if(is_array($resultado))
        {
          // Recorrer el arreglo y asignar las variables.
          foreach($resultado as $unidad)
          {
            $unidad->tiene_transporte = $unidad->tiene_transporte == "t";
			      $unidad->activa = $unidad->activa == "t";
          }
        }

        // Si el resultado es un objeto.
        else
        {
          $resultado->tiene_transporte = $resultado->tiene_transporte == "t";
		      $resultado->activa = $resultado->activa == "t";
        }

        return $resultado;
    }

    public function create($nombre, $departamento, $transporte)
    {
        // Datos de la creación.
        $data = array(
            'nombre' => $nombre,
            'departamento' => $departamento,
            'tiene_transporte' => $transporte,
            'activa' => true
        );

        return $this->db->insert('unidad_organizacional', $data);
    }

    public function update($id, $activa, $padre = 0)
    {
      // Asigna las variable, solo si son especificadas.
      $data = array();
      if($activa !== null) $data['activa'] = $activa;
      if($padre !== 0) $data['unidad_padre'] = $padre;

      // Actualiza con en los registros con el id especificado. 
      $this->db->where('id_unidad_organizacional', $id);
      return $this->db->update('unidad_organizacional', $data);
    }
}
